### Projeto em andamento

# LojaGeek  E-Commerce
O projeto criado usando a ferramenta ASP.Net MVC5, Razor e bootstrap. Construído para ser uma plataforma de e-commerce para venda de jogos eletrônicos, em mídia física. Tem como objetivo ser um site conciso, completo e com tendências de desenvolvimento atuais, como a resposividade e ux avançada.

## Iniciando o projeto
Abrir o arquivo .sln da raiz do projeto com o Visual Studio. Após carregar todas as configurações, aperte f5 para criar um servidor local e abrir o site no browser.

### Pré requisitos
- Sistema operacional windows.
- Visual Studio, recomendado o 2017

## Descrição do sistema
### Geral
- Site com estética baseada na bibliteca bootstrap
- Funcionamento aproximado de um site comum de e-commerce
- Um visitante pode navegar pela vitrine, pode criar um carrinho, pode ver os detalhes de um produto, pode comentar em um produto, só é limitada a compra.
- Um administrador pode fazer login, pode fazer tudo que um cliente pode fazer, exceto compra. Além de poder controlar o estoque através da página dedicada a esta função.
- O banco de dados está modelado de forma que o DER é este apresentado abaixo:

![DER](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/DER.PNG)
**DER do sistema sendo desenvolvido** 

#### Melhorias para o sistema em geral
- [ ] Melhor aplicação do bootstrap
- [ ] Melhor responsividade
- [ ] Criação de uma página para o perfil do cliente
- [ ] Criação de uma página para o cliente inserir um endereço, para entrega, tanto na página de compra quanto na de perfil
- [ ] Criação de uma ferramenta de administração de comentários para um administrador
- [ ] Criação de uma ferramenta de administração Para colocar jogos em vitrine de destaque

### Página Inicial
- A página inicial possuium menu fixo superior com o nome da loja e alguns item de menu.
- O nome da loja é clicável e leva a página incial, não importa aonde esteja no sistema.
- Análogo ao lado possui um botão com um símbolo em formato de casinha, que faz a mesma ação da logo.
- O botão cadastrar leva a um formulário para cadastro de um novo cliente.
- O botão entrar leva o cliente a um formulário de login.
- O símbolo de carrinho, leva a página de carrinho da sessão.
- Abaixo do menu há uma jumbotron para destacar três novos jogos.
- Abaixo fica a listagem de jogos, caso tenha sido adicionado algum.
- Embaixo de todos os produtos há dois botões, detalhes e um com glyphicon com um carrinho de compras.
- O botão detalhes te leva à página de detalhe do produto.
- O botão com um carrinho coloca o produto no carrinho. 
- Listagem de jogos começa do ponto central e espalha os jogos em forma igualada.
- Abaixo há o footer com um copyright, que é um link para a área administrativa.
- Ao clicar no link há um redirecionamento para a página de login administrativo.
#### Melhorias para a página inicial
- [ ] Produtos dinamicos no jumbotron
- [ ] Listagem de produtos mais concisa

![Página inicial sem nenhum cliente logado](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Inicial.png)
**Página inicial sem nenhum cliente logado**

![Página inicial com um cliente logado](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Inicial%20com%20cliente%20logado.png)
**Página inicial com um cliente logado**

### Página Login administrativo
- O menu permanece o mesmo da página inicial.
- Há um formulário centrlaizado com apenaas um input, um label e um botão.
- A senha administrativa é uma senha gerada automática levando em conta as datas e horas.
- Se a senha for incorreta será levada a tela inicial do sistema.
- Se a senha for correta será levado a tela de gerenciamento de estoque.
#### Melhorias para a pagina Login administrativo
- [ ] Limitar o uso de acento no corpo da senha gerada

![Página login administrativo](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Login%20aadministrativo.png)
**Página login administrativo**

### Página Estoque
- O menu agora é o admnistrativo, contendo um opção de sair e de navegação para a página de estoque, além das opções da logo e do símbolo de casa do menu já citado na página inicial.
- Há um botão Novo produto que leva a uma página de formulário para adição do produto.
- Abaixo há uma tabela com título produtos, uma linha de títulos, contendo foto, nome ,preço, ~~quantidade em~~ estoque , ativo e ~~ações~~.
- ~~Na coluna foto é carregado uma pequena thumbnail da foto associada ao produto.~~
- Na coluna nome será carregado o nome do produto.
- Na coluna preço será carregado o preço atual do produto.
- Na coluna ~~quantidade em~~ estoque será carregado a quantidade atual do produto.
- Na coluna ativo será mostrado se o produto está ativo e visível para compra ou não.
- Na coluna ~~ações~~ será carregado os ~~botões~~ links de ação para o produto, como "Destivar produto", "Ativar Produto", Adicionar ~~novo~~ estoque.
- O ~~botão~~ link Adicionar estoque leva a uma tela de formulário para adicionar mais produto ao estoque.
#### Melhorias para a página Estoque
- [ ] Corrigir as nomeações e aparências que estão riscadas acima, para questão de usabilidade.
- [ ] Implementar o carregamento da foto thumnail riscada acima.
- [ ] Impedir o ativamento do produto caso o estoque eteja zerado.
- [ ] Impedir o acesso de páginas administrativas através de link direto.
- [ ] Criar área para criação de cupoms.

![Página de estoque](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Controle%20de%20estoque.png)
**Página de estoque**

### Página Cadastro de Novo Produto
- Um formulário com título para cadastrar produto.
- Formulário contém nome, descrição, plataforma, estoque, preço, foto e um checkbox ativo e um botão para salvar produto
- O input de preço possui um placeholder explicando qual deve ser inserido.
- Todos esses inputs são informações para guardar um produto no banco de dados.
- ~~Há um link no fim do input para voltar para a listagem do estoque.~~
- Ao salvar o preço deve ser calculado automaticamente, levando em conta todos os custos por tráz e todos os impostos.
- Ao salvar o produto deve-se retornar a listagem de produtos na view de estoque.
#### Melhorias para a página Cadastro de Novo Produto
- [ ] Colocar o input de foto como um input file para upload de uma imagem de thumbnail.
- [ ] Colocar o input de plataforma como uma lista dropdown.
- [ ] Corrigir o link que está riscado acima.
- [ ] Modificar modelo para adicionar estilo do jogo.

![Página Cadastro de Novo Produto](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Cadastro%20produto.png)
**Página Cadastro de Novo Produto**

### Página Adicionar estoque
- Um formulário com título para Adicionar Estoque.
- Formulário contém nome, descrição, preenchido com o produto a ser adicionado no estoque.
- Inputs de estoque, preço editáveis, para preço das novas peças e quantidade do mesmo, há também um botão de adicionar estoque
- Há um link abaixo do formulário para voltar a tela de estoque
- Ao mandar adicionar no estoque, o preço real será calculado e feito uma média dos preços para o calculo do novo preço do produto, isso será calculado automaticamente.
#### Melhorias para a página Adicionar estoque
- [ ] Zerar os valores do inputs de estoque e preço para melhorar a usabilidade.
- [ ] Impedir input de números negativos.

![Página Adicionar estoque](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Repor%20estoque.png)
**Página Adicionar estoque**

### Página Cadastrar Cliente
- Um formulário com título para Cadastro Cliente.
- Formulário contém nome, Sobrenome, CPF, E-mail, Senha e checkbox de interesses.
- Abaixo do input tem um botão para cadastrar
- Ao enviar o cliente é gravado no banco de dados.
- Após o cliente ser salvo é levado a tela de login.
#### Melhorias para a página Cadastrar Cliente
- [ ] Máscara para o cpf.
- [ ] Input específico para email.
- [ ] Input para confirmar senha.
- [ ] Melhorar interesses.

![Página Cadastrar Cliente](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Cadastro%20cliente.png)
**Página Cadastrar Cliente**

### Página Login Cliente
- Possui um formulário com título login.
- Input para email e senha.
- Há um botão Enviar.
- Ao formulário ser preenchido e enviado é testado no banco de dados se existem no banco de dados
- Caso exista este é logado na sessão, o menu muda para um menu de cliente, com o nome dele como item, botão sair, carrinho de compras logo e simbolo home.
- Caso não exista o usuário esse é mandado de volta a página inicial.
#### Melhorias para a página Login Cliente
- [ ] Mensagem de erro caso login não bem sucessido.
- [ ] Link para cadastar cliente.

![Página Login Cliente](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Login%20cleinte.png)
**Página Login Cliente**


### Página Detalhe Produto
- A tela traz todas informações do produto exceto se está ativo ou não, pois isso é exclusivo da administração.
- Possui um textarea para colocar um comentário.
- Possui um input para colocar o nome da pessoa que vai comentar.
- Um botão para enviar o comentário
- Um botão para colocar o produto no carrinho
- Possui no fim da página uma lista de comentário do produto
#### Melhorias para a página Detalhe Produto
- [ ] Corrigir botão de enviar para o carrinho.
- [ ] Aproximar detalhe de outros e-commerce.

![Página Detalhe Produto](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Detalhes%20do%20produto.png)
**Página Detalhe Produto**

### Página do carrinho
- Uma tabela com título carrinho.
- Uma coluna com cabeçalho produto, ao qual possui o nome do produto.
- Uma coluna com cabeçalho preço, ao qual possui o preço do produto vezes a quantidade.
- Uma coluna com cabeçalho quantidade, ao qual possui a quantidade atual do produto, e um select com o número máximo do estoque do produto, um botão de atualizar total, que ao apertar atualiza a quantidade e os preços.
- Uma coluna com cabeçalho ~~ações~~, ao qual possui botão "Detalhes do produto", ao qual leva ao detalhe do produto, botão de retirar produto do carrinho.
- Uma área para o Valor Total do carrinho.
- Uma área para aplicar um cupom, com mensagem de erro caso não seja possível aplicar o cupom.
- Uma área para aplicar frete, com mensagem de quanto é o frete.
- No fim, caso não haja usuário logado, um botão cadastrar e um entrar, que leva para view cadastro de cliente e view login respectivamente.
- No fim, caso haja usuário logado, um botão comprar que leva a view de confirmação de compra.
#### Melhorias para a página do carrinho
- [ ] Melhorar mensagens de erro e sucesso.
- [ ] Criar view de carrinho vazio.

!Página do carrinho sem cliente logado](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Carrinho%20deslogado.png)
**Página do carrinho sem cliente logado*

![Página do carrinho com cliente logado](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Carrinho%20logado.png)
**Página do carrinho com cliente logado*

### Página de compra
- Uma página estática com texto falando o nome, cpf, com data da compra, items comprado com suas quantidades e preço, valor total da compra e instruções finais da compra.
#### Melhorias para a página de compra
- [ ] Colocar uma view intermediária para cadastro de endereço.
- [ ] Fazer uma aparência de nota fiscal.

![Página de compra](https://github.com/GabrielDSousa/LojaGeek/blob/master/Documentação/Imagens/Comprovante%20de%20compra.png)
**Página de compra**

## Construído com as ferramentas
* [Asp.Net MVC 5](https://docs.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/introduction/getting-started) - O framework back-end utilizado
* [Razor](http://jakeydocs.readthedocs.io/en/latest/mvc/views/razor.html) - O framework web utilizado
* [Bootstrap](https://getbootstrap.com/docs/4.1/getting-started/introduction/) - CSS Framework utilizado
* [JQuery](https://api.jquery.com/) - JavaScript framework
* [NHibernate](http://hibernate.org/orm/documentation/5.2/) - Framework de banco de dados utilizado


## Autores
* **Gabriel Ramos de Sousa** - *Atual mantenedor do projeto, Mauntenção do sistema, refatoração e continuação de senvolvimento* - [GabrielDSousa](https://github.com/GabrielDSousa)
* **Danilo Itagyba** - *Iníciou o projeto* - [itagyba](https://github.com/itagyba)

## Licensa
This project is licensed under the MIT License

## Agradecimentos
* Ao professor Tadeu, por guiar no projeto, com requerimentos incrementais e semanais.
