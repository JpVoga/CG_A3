# Galeria interativa de 10 objetos 3D em Unity.

## Autores
 * João Pedro Voga de Oliveira - RA: 1262216592
 * Leonardo Loiola Loureiro - RA: 1262222969

## Visão geral:
 * Trata-se de uma galeria de 10 objetos 3D onde se pode trocar o objeto em foco e mover a câmera, por meio das setas e pelo mouse, respectivamente.

## Classes:
 * Gallery: Representa o objeto pai que contém todos os objetos. Essa classe contém a lista dos objetos e o índice do objeto em foco. Essa classe escuta eventos da classe GameInput para realizar as ações da câmera.
 * GalleryObject: Contém as informações sobre seu respectivo objeto. Especiicamente, relaciona o objeto com sua câmera.
 * GameInput: Dispara eventos que vão ser ouvidos por outras classes quando as ações de controle são executadas pelo usuário.

## Como executar:
 * O executável se encontra na pasta Build, é o arquivo CG_A3.exe
