export interface IBookResponse {
  cod: number;

  titulo: string;

  editora: string;

  edicao: number;

  anoPublicacao: string;

  preco: number;

  autores: Array<string>;

  assuntos: Array<string>;
}
