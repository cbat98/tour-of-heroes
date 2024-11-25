import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';
import { Md5 } from 'ts-md5';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})
export class HeroDetailComponent implements OnInit {
  @Input() hero?: Hero;
  referenceHero?: Hero;
  isEditing = false;

  constructor(
    private route: ActivatedRoute,
    private heroService: HeroService,
    private location: Location
  ) {
    this.hero = {
      id: 0,
      name: "Hero"
    }
    this.referenceHero = {
      id: this.hero.id,
      name: this.hero.name
    };
  }

  ngOnInit(): void {
    this.getHero();
  }

  getIdHash(): string {
    return Md5.hashStr((this.hero?.id ?? 0).toString());
  }

  getHero(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.heroService.getHero(id).subscribe(hero => {
      this.hero = hero;
      this.referenceHero = {
        id: hero.id,
        name: hero.name,
      };
    });
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    if (this.hero && this.isEditing) {
      this.heroService.updateHero(this.hero).subscribe(_ => this.toggleEditing());
    }
  }

  delete(): void {
    if (this.hero) {
      this.heroService.deleteHero(this.hero.id).subscribe(_ => this.goBack());
    }
  }

  toggleEditing() {
    this.isEditing = !this.isEditing;
  }

  cancelEdit(): void {
    console.log(this.referenceHero);
    console.log(this.hero);

    this.hero = {
      id: this.referenceHero?.id ?? 0,
      name: this.referenceHero?.name ?? "Hero"
    }
    this.toggleEditing();
  }
}
