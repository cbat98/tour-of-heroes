import { Component } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  heroes$: Observable<Hero[]> = of([]);
  heroes: Hero[] = [];

  constructor(private heroService: HeroService) {
    this.heroes.push({id: 0, name: "Hero"})
  }

  ngOnInit(): void {
    this.getHeroes();
  }

  getHeroes(): void {
    this.heroes$ = this.heroService.getHeroes();
    this.heroes$.subscribe(heroes => this.heroes = heroes.slice(1, 5));
  }
}
