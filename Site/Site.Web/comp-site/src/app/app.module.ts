import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingComponent } from './pages/landing/landing.component';
import { NavigationComponent } from './core/navigation/navigation.component';
import { HeroComponent } from './components/landing/hero/hero.component';
import { CompetitionSummaryComponent } from './components/landing/competition-summary/competition-summary.component';
import { SponsorsListComponent } from './components/landing/sponsors-list/sponsors-list.component';
import { SponsorsItemComponent } from './components/landing/sponsors-item/sponsors-item.component';
import { MentorsListComponent } from './components/landing/mentors-list/mentors-list.component';
import { MentorCardComponent } from './components/landing/mentor-card/mentor-card.component';

@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    NavigationComponent,
    HeroComponent,
    CompetitionSummaryComponent,
    SponsorsListComponent,
    SponsorsItemComponent,
    MentorsListComponent,
    MentorCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
