### Projeto em andamento

# LojaGeek  E-Commerce
O projeto criado usando a ferramenta ASP.Net MVC5, Razor e bootstrap. Construído para ser uma plataforma de e-commerce para venda de jogos eletrônicos, em mídia física. Tem como objetivo ser um site conciso, completo e com tendências de desenvolvimento atuais, como a resposividade e ux avançada.

## Iniciando o projeto
Abrir o arquivo .sln da raiz do projeto com o Visual Studio. Após carregar todas as configurações, aperte f5 para criar um servidor local e abrir o site no browser.

### Prerequisitos
- Sistema operacional windows.
- Visual Studio, recomendado o 2017

## Descrição do sistema
### Geral
- Site com estética baseada na bibliteca bootstrap
- Funcionamento aproximado de um site comum de e-commerce
- Um visitante pode navegar pela vitrine, pode criar um carrinho,, pode ver os detalhes de um produto, pode comentar em um produto, só é limitada a compra.
- Um administrador pode fazer login, pode fazer tudo que um cliente pode fazer, exceto compra. Além de poder controlar o estoque através da página dedicada a esta função

#### Melhorias para o sistema em geral
- [ ] Melhor aplicação do bootstrap
- [ ] Melhor responsividade
- [ ] Criação de uma página para o perfil do cliente
- [ ] Criação de uma página para o cliente inserir um endereço, para entrega, tanto na página de compra quanto na de perfil
- [ ] Criação de uma ferramenta de administração de comentários para um administrador
- [ ] Criação de uma ferramenta de administração Para colocar jogos em vitrine de destaque

### Página Inicial
- A página inicial consiste em um menu branco superior com o nome da loja e alguns item de menu.
- O nome da loja é clicável e leva a página incial, não importa aonde esteja no sistema.
- Análogo ao lado possui um botão com um símbolo em formato de casinha, que faz a mesma ação da logo.
- O botão cadastrar leva a um formulário para cadastro de um novo cliente.
- O botão entrar leva o cliente a um formulário de login.
- O símbolo de carrinho, leva a página de carrinho da sessão
- Abaixo do menu há uma jumbotron para destacar três novos jogos.
- Abaixo fica a listagem de jogos, caso tenha sido adicionado algum.
- Listagem de jogos começa do ponto central e espalha os jogos em forma igualada.
- Abaixo há o footer com um copyright, que é um link para a área administrativa.
- Ao clicar no link há um redirecionamento para a página de login administrativo.

#### Melhorias para a página inicial
- [ ] Produtos dinamicos no jumbotron
- [ ] Listagem de produtos mais concisa

### Página Login administrativo
- O menu permanece o mesmo da página inicial.
- Há um formulário centrlaizado com apenaas um input, um label e um botão.
- A senha administrativa é uma senha gerada automática levando em conta as datas e horas.
- Se a senha for incorreta será levada a tela inicial do sistema.
- Se a senha for correta será levado a tela de gerenciamento de estoque.
#### Melhorias para a página inicial
- [ ] Limitar o uso de acento no corpo da senha gerada

### Página Estoque
- O menu agora é o admnistrativo, contendo um opção de sair e de navegação para a página de estoque, além das pção da logo e do símbolo de casa do menu já citado na página inicial.
- Há um botão Novo produto que leva a uma página de formulário para adição do produto.
- Abaixo há uma tabela com título produtos, uma linha de títulos, contendo foto, nome ,preço, ~~quantidade em~~ estoque , ativo e ~~ações~~.
- ~~Na coluna foto é carregado uma pequena thumbnail da foto associada ao produto.~~
- Na coluna nome será carregado o nome do produto.
- Na coluna preço será carregado o preço atual do produto.
- Na coluna ~~quantidade em~~ estoque será carregado a quantidade atual do produto.
- Na coluna ativo será mostrado se o produto está ativo e visível para compra ou não.
- Na coluna ~~ações~~ será carregado os ~~botões~~ links de ação para o produto, como "Destivar produto", "Ativar Produto", Adicionar ~~novo~~ estoque.
- O ~~botão~~ link Adicionar estoque leva a uma tela de formulário para adicionar mais produto ao estoque.
#### Melhorias para a página inicial
- [ ] Corrigir as nomeações e aparências que estão riscadas acima, para questão de usabilidade.
- [ ] Implementar o carregamento da foto thumnail riscada acima.
- [ ] Impedir o ativamento do produto caso o estoque eteja zerado.


