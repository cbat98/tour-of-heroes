import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css']
})
export class HeroesComponent implements OnInit {
  heroes: Hero[] = [];
  displayedColumns: string[] = ["id", "name", "select"];
  @ViewChild(MatTable) table!: MatTable<Hero>;
  selection = new SelectionModel<Hero>(true, []);

  constructor(private heroService: HeroService) {
    this.heroes.push({ id: 0, name: "Hero" })
  }

  ngOnInit(): void {
    this.getHeroes();
  }

  getHeroes(): void {
    this.heroService.getHeroes().subscribe(heroes => this.heroes = heroes);
  }

  toggleAllRows(): void {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }
    this.selection.select(...this.heroes);
  }

  isAllSelected(): boolean {
    const numSelected = this.selection.selected.length;
    const numRows = this.heroes.length;
    return numSelected === numRows;
  }

  isAnySelected(): boolean {
    return this.selection.selected.length === 0;
  }

  add(name: string) {
    name = name.trim();
    if (!name) {
      return;
    }

    this.heroService.addHero({ name } as Hero).subscribe(hero => {
      if (hero != null) {
        this.heroes.push(hero);
        this.table.renderRows();
      }
    });
  }

  deleteSelected(): void {
    this.selection.selected.forEach(hero => {
      this.delete(hero);
    });
  }

  delete(hero: Hero) {
    this.heroService.deleteHero(hero.id).subscribe(_ => {
      this.heroes = this.heroes.filter(h => h !== hero);
      this.table.renderRows();
    });
  }
}
