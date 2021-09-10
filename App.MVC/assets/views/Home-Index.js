$(function () {
    $('#btnTutorial').on('click', function (e) {
        try {
            e.preventDefault();

            var _slides = [{
                content: 'Clique aqui para baixar o atalho, salve o atalho na sua área de trabalho para facilitar o acesso ao sistema.',
                position: 'bottom-center',
                overlayMode: 'focus',
                selector: '#downloadAtalho',
                title: 'Download',
                arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
            },
                {
                    content: 'Aqui está a agenda do seu salão, todos os dados de clientes já agendados e de agendamentos futuros tudo em um só lugar.',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuAgenda',
                    title: 'Sua agenda',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Aqui estão todas as suas comandas, você pode criar uma comanda e controlar melhor tudo que entra e sai do seu salão.',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuComanda',
                    title: 'Suas comandas',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Aqui estão todas as suas finanças, aqui você terá o controle de todas suas receitas e despesas.',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuFinanceiro',
                    title: 'Suas finanças',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Aqui estão as comissões que estão abertas, elas são calculadas automaticamente pelo sistema.',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuComissoes',
                    title: 'Suas comissões',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Nesse menu você poderá cadastrar todos os dados básicos do seu salão, por exemplo:<br><br><ul><li>Clientes</li><li>Profissionais</li><li>Serviços</li><li>Produtos</li><li>Usuários</li></ul>',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuMeuSalao',
                    title: 'Seu Salão',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Nesse menu você terá acesso a todos os relatórios do seu salãp, por exemplo:<br><br><ul><li>Fechamento de caixa</li><li>Movimentações financeiras</li><li>Agendamentos</li><li>etc...</li></ul>',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuRelatorios',
                    title: 'Relatórios',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Aqui estão todos os dados do seu salão, tudo em um só lugar, um resumo de tudo o que aconteceu e que está acontecendo em seu salão.',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuDashboards',
                    title: 'Dashboards',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Aqui o sistema reune todas as suas informações pessoais e da empresa, também temos um menu ajuda para lhe ajudar em qualquer dúvida.',
                    position: 'right-center',
                    overlayMode: 'focus',
                    selector: '#menuConfiguracoes',
                    title: 'Configurações',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                },
                {
                    content: 'Realize a configuração inicial usando esse guia.',
                    position: 'left-center',
                    overlayMode: 'focus',
                    selector: '.panel-warning',
                    title: 'Configuração inicial',
                    arrowPath: '../../assets/global/plugins/tutorialize/arrow-blue.png',
                }
            ];

            $.tutorialize({
                slides: _slides,
                autoScroll: true,
                labelEnd: 'Fechar',
                labelNext: 'Próximo',
                labelPrevious: 'Anterior',
                labelStart: 'Próximo',
            });

            $.tutorialize.start();

        } catch (e) {
            alert(e)
        }
    });
});