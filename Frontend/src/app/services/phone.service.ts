import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Phone } from '../models/phone.model';

@Injectable({
  providedIn: 'root'
})
export class PhoneService {
  private apiUrl = 'http://localhost:5218/api/phones';

  constructor(private http: HttpClient) { }

  getPhones(): Observable<Phone[]> {
    return this.http.get<Phone[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  getPhone(id: number): Observable<Phone> {
    return this.http.get<Phone>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  createPhone(phone: Phone): Observable<Phone> {
    return this.http.post<Phone>(this.apiUrl, phone)
      .pipe(catchError(this.handleError));
  }

  updatePhone(id: number, phone: Phone): Observable<Phone> {
    return this.http.put<Phone>(`${this.apiUrl}/${id}`, phone)
      .pipe(catchError(this.handleError));
  }

  deletePhone(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side error
      if (error.status === 400 && error.error?.errors) {
        // Handle validation errors
        const validationErrors = [];
        for (const key in error.error.errors) {
          if (error.error.errors.hasOwnProperty(key)) {
            validationErrors.push(error.error.errors[key]);
          }
        }
        errorMessage = validationErrors.join(', ');
      } else {
        errorMessage = `Error Code: ${error.status}, Message: ${error.message}`;
      }
    }
    
    return throwError(() => new Error(errorMessage));
  }
}