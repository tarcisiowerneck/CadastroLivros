import { Component } from '@angular/core';
import { BooksService } from '../../../services/books.service';
import { IBooksResponse } from '../../../models/books-response.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.css'
})

export class BooksListComponent {

  public books: IBooksResponse[] = []

  private temp: object[] = []

  cod: number = 0;

  constructor(private BooksService: BooksService, private router: Router) {
    this.getBooks();
  }

  editBook($event: any) {
    this.router.navigate(['books/edit/', Number($event)]);
  }

  removeBook($event: any) {
    this.BooksService.removeBook(Number($event))
      .subscribe({
        next: (response) => {
          this.getBooks();
          this.router.navigate(['books/']);
        },
        error: (error) => {
          alert('Erro ao remover o livros.');
        }
      });    
  }

  private getBooks() {
    this.BooksService.getBooks()
      .subscribe({
        next: (response) => {
          this.books = response;
        },
        error: (error) => {
          alert('Erro ao listar os livros.');
        }
      });
  }
}
