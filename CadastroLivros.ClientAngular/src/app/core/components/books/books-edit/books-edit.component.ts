import { Component, OnInit } from '@angular/core';
import { IBookRequest } from '../../../models/book-request.model';
import { BooksService } from '../../../services/books.service';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books-edit',
  templateUrl: './books-edit.component.html',
  styleUrl: './books-edit.component.css'
})
export class BooksEditComponent implements OnInit {

  model: IBookRequest;

  autor: string;
  assunto: string;

  constructor(private BooksService: BooksService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.model = {
      Cod: 0,
      Titulo: '',
      Editora: '',
      Edicao: 0,
      AnoPublicacao: '',
      Preco: 0,
      Autores: [],
      Assuntos: []
    };
    this.autor = '';
    this.assunto = '';
  }

  ngOnInit(): void {
    let bookCod = Number(this.activatedRoute.snapshot.paramMap.get("cod"));
    this.getBook(bookCod);
  }

  addAutores() {
    if (this.autor.trim().length !== 0) {
      this.model.Autores.push(this.autor);
    }
    this.autor = '';
  }

  addAssuntos() {
    if (this.assunto.trim().length !== 0) {
      this.model.Assuntos.push(this.assunto);
    }
    this.assunto = '';
  }

  onFormSubmit() {
    this.BooksService.addBook(this.model)
      .subscribe({
        next: () => {
          this.router.navigateByUrl('/books/list');
        },
        error: (error) => {
          alert('Não foi possível editar o livro, verifique os dados e tente novamente.');
        }
      });
  }

  private getBook(cod: number) {
    this.BooksService.getBook(cod)
      .subscribe({
        next: (response) => {
          this.model.Cod = response.cod;
          this.model.Titulo = response.titulo;
          this.model.Editora = response.editora;
          this.model.Edicao = response.edicao;
          this.model.AnoPublicacao = response.anoPublicacao;
          this.model.Preco = response.preco;
          this.model.Autores = response.autores;
          this.model.Assuntos = response.assuntos;
        },
        error: (error) => {
          alert(error);
        }
      });
  }
}
