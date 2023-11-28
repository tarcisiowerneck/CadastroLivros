import { Injectable } from '@angular/core';
import { IBookRequest } from '../models/book-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IBooksResponse } from '../models/books-response.model';
import { IBookResponse } from '../models/book-response.model';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  constructor(private http: HttpClient) { }

  addBook(model: IBookRequest) : Observable<void> {

    return this.http.post<void>('https://localhost:44343/api/livros', model);

  }

  getBooks(): Observable<IBooksResponse[]> {

    return this.http.get<IBooksResponse[]>('https://localhost:44343/api/livros');

  }

  getBook(cod: number): Observable<IBookResponse> {

    return this.http.get<IBookResponse>('https://localhost:44343/api/livros/' + cod);

  }

  removeBook(cod: number): Observable<void> {

    return this.http.delete<void>('https://localhost:44343/api/livros/' + cod);

  }

}
