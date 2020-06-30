import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Reserva } from '../models/reserva/reserva';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ReservaService {
  url = 'http://localhost:5000/api/reserva';

  constructor(private httpClient: HttpClient) {}

  // Obtem todas reservas
  getReserva(): Observable<Reserva[]> {
    return this.httpClient
      .get<Reserva[]>(this.url)
      .pipe(retry(2), catchError(this.handleError));
  }

  // Obtem todas reservas por empresa
  getReservaEmpresa(): Observable<Reserva[]> {
    return this.httpClient
      .get<Reserva[]>(this.url + '/empresa')
      .pipe(retry(2), catchError(this.handleError));
  }

  // Criar uma reserva
  saveReserva(reserva: Reserva): Observable<Reserva> {
    return this.httpClient
      .post<Reserva>(this.url, JSON.stringify(reserva))
      .pipe(retry(2), catchError(this.handleError));
  }

  // Atualiza uma reserva
  updateReserva(reserva: Reserva): Observable<Reserva> {
    return this.httpClient
      .put<Reserva>(this.url + '/' + reserva.id, JSON.stringify(reserva))
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
