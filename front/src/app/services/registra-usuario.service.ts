import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Usuario } from '../models/usuario/usuario';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class RegistraUsuarioService {
  url = 'http://localhost:5000/api/usuario';

  constructor(private httpClient: HttpClient) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': 'http://localhost:4200',
    }),
  };

  // Criar um usuario
  saveUsuario(usuario: Usuario): Observable<Usuario> {
    return this.httpClient
      .post<Usuario>(this.url, JSON.stringify(usuario), this.httpOptions)
      .pipe(retry(2), catchError(this.handleError));
  }

  // Atualiza um usuario
  updateUsuario(usuario: Usuario): Observable<Usuario> {
    return this.httpClient
      .put<Usuario>(
        this.url + '/' + usuario.id,
        JSON.stringify(usuario),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  // deleta uma empresa
  deleteUsuario(usuario: Usuario) {
    return this.httpClient
      .delete<Usuario>(this.url + '/' + usuario.id, this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }

  // Manipulação de erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage =
        `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
