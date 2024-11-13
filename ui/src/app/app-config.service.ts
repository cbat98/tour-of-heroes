import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from 'src/app/app-config.type';
import { tap, catchError, of, Observable, map } from 'rxjs';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  private appConfig?: AppConfig;

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  loadAppConfig() {
    return this.http.get<AppConfig>('/assets/config.json')
      .pipe(
        map((config: AppConfig) => this.appConfig = config),
        tap(_ => this.log('loaded config')),
        catchError(this.handleError<AppConfig>('loadAppConfig'))
      )
  }

  getBaseUrl(): string {
    return `${this.appConfig?.apiHost}/${this.appConfig?.apiRoute}`
  }

  private log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }
}
