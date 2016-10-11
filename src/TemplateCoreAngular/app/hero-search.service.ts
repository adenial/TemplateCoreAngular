import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import { Hero } from './hero';


@Injectable()
export class HeroSearchService
{
  private heroesUrl = 'api/searchheroes';
  private headers = new Headers({ 'Content-Type': 'application/json' });
  constructor(private http: Http) { }

  search(term: string): Observable<Hero[]>
  {
    const url = `${this.heroesUrl}/${term}`;
    return this.http
      .get(url, { headers: this.headers })
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