import './rxjs-extensions';
import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent }  from './app.component';
import { RouterModule }   from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { HeroesComponent } from './heroes.component';
import { HeroDetailComponent } from './hero-detail.component';
import { HeroService } from './hero.service';
import { HeroSearchComponent } from './hero-search.component';

@NgModule(
  {
    imports: [BrowserModule, FormsModule, HttpModule,
      RouterModule.forRoot([
        {
          path: 'heroes',
          component: HeroesComponent
        },
        {
          path: 'dashboard',
          component: DashboardComponent
        },
        {
          path: '',
          redirectTo: '/dashboard',
          pathMatch: 'full'
        },
        {
          path: 'detail/:id',
          component: HeroDetailComponent
        }
      ])
    ],
    declarations: [AppComponent, HeroDetailComponent, HeroesComponent, DashboardComponent, HeroSearchComponent],
    providers: [HeroService],
    bootstrap: [AppComponent]
  })

export class AppModule
{
}