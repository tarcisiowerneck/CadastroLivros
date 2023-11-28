import { Component } from '@angular/core';
import { IBookRequest } from '../../../models/book-request.model';
import { BooksService } from '../../../services/books.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books-add',
  templateUrl: './books-add.component.html',
  styleUrl: './books-add.component.css'
})
export class BooksAddComponent {

  model: IBookRequest;

  autor: string;
  assunto: string;

  constructor(private BooksService: BooksService, private router: Router) {
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
        next: (response) => {
          this.router.navigate(['/books/list']);
        },
        error: (error) => {
          alert('Não foi possível adicionar o livro, verifique os dados e tente novamente.');
        }
      });
  }

}
