import { Injectable } from "@angular/core";
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';

import 'rxjs/add/operator/toPromise';

import { Hero } from './hero';

@Injectable()
export class HeroService
{
  // URL to web api
  private heroesUrl = 'api/heroes';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http)
  {
  }

  getHeroes(): Observable<Hero[]>
  {
    return this.http
      .get(this.heroesUrl)
      .map(this.extractData)
      .catch(this.handleError);
  }

  update(hero: Hero): Observable<Hero>
  {
    const url = `${this.heroesUrl}/${hero.id}`;
    return this.http
      .put(url, JSON.stringify(hero), { headers: this.headers })
      .map(() => hero)
      .catch(this.handleError);
  }

  delete(id: number): Observable<void>
  {
    const url = `${this.heroesUrl}/${id}`;
    return this.http
      .delete(url, { headers: this.headers })
      .map(() => null)
      .catch(this.handleError);
  }

  getHero(id: number): Observable<Hero>
  {
    return this.http
      .get(this.heroesUrl + '/' + id)
      .map(this.extractData)
      .catch(this.handleError);
  }

  create(name: string): Observable<Hero>
  {
    return this.http
      .post(this.heroesUrl, JSON.stringify({ name: name }), { headers: this.headers })
      .map(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response)
  {
    let body = res.json();
    return JSON.parse(body);
  }

  private handleError(error: any): Observable<any>
  {
    console.error('An error ocurred', error);
    return error;
  }
}