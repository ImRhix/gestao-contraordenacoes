# GeCO - Gestão de Contra Ordenações

Protótipo de uma aplicação cross-platform para gerir situações relacionadas com o trânsito e outras ocorrências na via pública, como acidentes, colisões etc.

  A aplicação foi desenvolvida com Xamarin.Forms em C# e a informação é guardada localmente com SQLite. Numa versão posterior quaisquer alterações de informações (modificar, apagar ou inserir novos autos / pessoas, transações, legislações etc) terão de ser obtidas e sincronizadas com o servidor da entidade responsável, sendo indispensável uma conexão à internet para poder utilizar a app.


## Getting Started

Para utilizar a aplicação basta clonar ou fazer o download do ZIP com o projeto e correr a solução no Visual Studio. Para informações mais detalhadas ver as secções Pré-requesitos e Instalação.


### Screenshots

Alguns exemplos do aspeto atual da aplicação.

<img src="https://raw.githubusercontent.com/ImRhix/gestao-contraordenacoes/master/screenshots/loginScreen.png" alt="Login Screen" width="270" /> <img src="https://raw.githubusercontent.com/ImRhix/gestao-contraordenacoes/master/screenshots/listScreen.png" alt="Lista de Autos" width="270" /> <img src="https://raw.githubusercontent.com/ImRhix/gestao-contraordenacoes/master/screenshots/closedFormScreen.png" alt="Form Inicial" width="270" />


### Pré-requisitos

Para correr o projeto será necessário:

```
- Visual Studio (recomenda-se a versão 2017);
- Ter o Xamarin instalado;
- Dispositivo Android 4.4 ou superior (não é necessário mas é preferível ao emulador)
```

### Instalação

Se o Xamarin já estiver instalado no Visual Studio pode saltar diretamente para o passo 4.

1. Abrir o Visual Studio Installer;
2. Clicar em More > Modify > selecionar 'Moblie development with .NET' > Modify/Install.

3. Realizar o download e fazer unzip da pasta ([pode clicar aqui para obter o ZIP](https://github.com/ImRhix/gestao-contraordenacoes));
4. Abrir o Visual Studio (2017 recomendado);
5. Abrir a solução do projeto, clicando na barra de menu e selecionando: File > Open > Project/Solution > (navegar até à pasta do projeto) > GeCO.sln ;

Estes passos serão suficientes para abrir o projeto e editar o código.

Para correr a aplicação basta selecionar o projeto GeCO.Android e carregar F5. Se desejar correr a aplicação num dispositivo físico ou num emulador diferente do default é provável que seja necessário o download da SDK/API correspondente, por exemplo, para correr o Android 7.0 será necessária a API 24. Os próximos passos explicam o download da SDK correspondente ao Android 7.0 (o processo é o mesmo para qualquer outra versão):

6. Na barra de menu selecionar: Tools > Android > Android SDK Manager.. ;
7. Na nova janela, no separador 'Platforms' expandir a opção 'Android 7.0 - Nougat' (cujo API level é 24). Selecionar a checkbox 'Android SDK Platform 24' ;h
8. Repetir o procedimento para qualquer outra versão selecionando sempre a checkbox 'Android SDK Platform XX' (onde XX representa o nível da API desejada).

## License



