(() => {
    const filtroStatus = document.getElementById('filtroStatus');
    const filtroPrioridade = document.getElementById('filtroPrioridade');
    const agrupamentoPrioridade = document.getElementById('agrupamentoPrioridade');
    const listaTarefas = document.getElementById('listaTarefas');
    const gruposTarefas = document.getElementById('gruposTarefas');
    const mensagemFiltroVazio = document.getElementById('mensagemFiltroVazio');
    const cards = Array.from(document.querySelectorAll('[data-tarefa-card]'));

    if (!filtroStatus || cards.length === 0)
        return;

    const atendeFiltros = (card) => {
        const statusSelecionado = filtroStatus.value;
        const prioridadeSelecionada = filtroPrioridade.value;

        const statusValido = !statusSelecionado || card.dataset.status === statusSelecionado;
        const prioridadeValida = !prioridadeSelecionada || card.dataset.prioridade === prioridadeSelecionada;

        return statusValido && prioridadeValida;
    };

    const atualizarListagem = () => {
        const agrupar = agrupamentoPrioridade.value === 'prioridade';
        let totalVisivel = 0;
        const contagensPorPrioridade = {};

        listaTarefas.classList.toggle('d-none', agrupar);
        gruposTarefas.classList.toggle('d-none', !agrupar);

        document.querySelectorAll('[data-grupo-prioridade]').forEach((grupo) => {
            contagensPorPrioridade[grupo.dataset.grupoPrioridade] = 0;
            grupo.classList.add('d-none');
            grupo.querySelector('[data-grupo-contador]').textContent = '0 tarefa(s)';
        });

        cards.forEach((card) => {
            const visivel = atendeFiltros(card);
            card.classList.toggle('d-none', !visivel);

            if (!visivel)
                return;

            totalVisivel++;

            if (agrupar) {
                const grupo = document.querySelector(`[data-grupo-prioridade="${card.dataset.prioridade}"]`);
                const listaGrupo = grupo.querySelector('[data-grupo-lista]');
                const contador = grupo.querySelector('[data-grupo-contador]');
                const totalGrupo = ++contagensPorPrioridade[card.dataset.prioridade];

                listaGrupo.appendChild(card);
                grupo.classList.remove('d-none');
                contador.textContent = `${totalGrupo} tarefa(s)`;
            } else {
                listaTarefas.appendChild(card);
            }
        });

        mensagemFiltroVazio.classList.toggle('d-none', totalVisivel > 0);
    };

    filtroStatus.addEventListener('change', atualizarListagem);
    filtroPrioridade.addEventListener('change', atualizarListagem);
    agrupamentoPrioridade.addEventListener('change', atualizarListagem);
})();
