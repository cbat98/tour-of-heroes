import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Hero } from './hero';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
   private httpOptions = {
     headers: new HttpHeaders({ 'Content-Type': 'application/json' })
   };

   private heroApiUrls = {
     heroes: `${this.appConfigService.getBaseUrl()}/heroes`,
   }

  constructor(
    private http: HttpClient,
    private messageService: MessageService,
    private appConfigService: AppConfigService
  ) { }

  getHeroes(): Observable<Hero[]> {
    console.table(this.heroApiUrls);
    return this.http.get<Hero[]>(this.heroApiUrls.heroes)
      .pipe(
        tap(_ => this.log('fetched heroes')),
        catchError(this.handleError<Hero[]>('getHeroes', []))
      );
  }

  getHero(id: number): Observable<Hero> {
    const url = `${this.heroApiUrls.heroes}/${id}`;
    return this.http.get<Hero>(url)
      .pipe(
        tap(_ => this.log(`fetched hero id=${id}`)),
        catchError(this.handleError<Hero>(`getHero id=${id}`))
      )
  }

  updateHero(hero: Hero): Observable<Hero> {
    const url = `${this.heroApiUrls.heroes}/${hero.id}`
    return this.http.put<Hero>(url, hero, this.httpOptions)
      .pipe(
        tap(_ => this.log(`updated hero id=${hero.id}`)),
        catchError(this.handleError<Hero>('updateHero'))
      )
  }

  addHero(hero: Hero): Observable<Hero> {
    return this.http.post<Hero>(this.heroApiUrls.heroes, hero, this.httpOptions)
      .pipe(
        tap((newHero: Hero) => this.log(`added hero id=${newHero.id}`)),
        catchError(this.handleError<Hero>(`addHero`))
      )
  }

  deleteHero(id: number): Observable<Hero> {
    const url = `${this.heroApiUrls.heroes}/${id}`;
    return this.http.delete<Hero>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log(`deleted hero id=${id}`)),
        catchError(this.handleError<Hero>('deleteHero'))
      )
  }

  searchHeroes(term: string): Observable<Hero[]> {
    if (!term.trim()) {
      return of([]);
    }

    return this.http.get<Hero[]>(`${this.heroApiUrls.heroes}/?name=${term}`)
    .pipe(
      tap(x => x.length
          ? this.log(`found ${x.length} heroes matching ${term}`)
          : this.log(`no heroes matching ${term}`)),
      catchError(this.handleError<Hero[]>('searchHeroes'))
    );
  }

  private log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }
}
