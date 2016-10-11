import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Hero } from './hero';
import { HeroService } from './hero.service';

@Component(
  {
    moduleId: module.id,
    selector: 'my-heroes',
    templateUrl: 'heroes.component.html',
    styleUrls: ['heroes.component.css']
  })

export class HeroesComponent implements OnInit
{
  constructor(private heroService: HeroService, private router: Router)
  {
  }

  selectedHero: Hero;
  heroes: Hero[];

  ngOnInit(): void
  {
    this.getHeroes();
  }

  getHeroes(): void
  {
    this.heroService
      .getHeroes()
      .subscribe(heroes => this.heroes = heroes);
  }

  onSelect(hero: Hero): void
  {
    this.selectedHero = hero;
  }

  gotoDetail(): void
  {
    this.router.navigate(['/detail', this.selectedHero.id]);
  }

  delete(hero: Hero): void
  {
    this.heroService.delete(hero.id)
      .subscribe(() =>
      {
        this.heroes = this.heroes.filter(h => h !== hero);
        if (this.selectedHero === hero)
        {
          this.selectedHero = null;
        }
      });
  }

  add(name: string): void
  {
    name = name.trim();
    if (!name)
    {
      return;
    }

    this.heroService.create(name)
      .subscribe(hero =>
      {
        this.heroes.push(hero);
        this.selectedHero = null;
      });
  }
}