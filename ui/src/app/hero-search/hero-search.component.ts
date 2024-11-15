import { Component, Input, OnInit } from '@angular/core';
import { map, startWith, Observable, of } from 'rxjs';
import { FormControl } from '@angular/forms';
import { Hero } from '../hero';

@Component({
  selector: 'app-hero-search',
  templateUrl: './hero-search.component.html',
  styleUrls: ['./hero-search.component.css']
})
export class HeroSearchComponent implements OnInit {
  @Input() heroes$: Observable<Hero[]> = of([]);
  searchControl = new FormControl<string | Hero>('');
  heroes: Hero[] = [];
  filteredHeroes: Observable<Hero[]> = of([]);

  ngOnInit() {
    this.heroes$.subscribe(heroes => this.heroes = heroes);
    this.filteredHeroes = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value?.name;
        return name ? this.filter(name as string) : this.heroes.slice();
      }),
    );
  }

  displayFn(hero: Hero): string {
    return hero && hero.name ? hero.name : '';
  }

  private filter(name: string): Hero[] {
    const filterValue = name.toLowerCase();

    return this.heroes.filter(option => option.name.toLowerCase().includes(filterValue));
  }

}
