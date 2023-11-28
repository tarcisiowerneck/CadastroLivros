export interface IBookRequest {
  Cod: number;

  Titulo: string;

  Editora: string;

  Edicao: number;

  AnoPublicacao: string;

  Preco: number;

  Autores: Array<string>;

  Assuntos: Array<string>;
}
