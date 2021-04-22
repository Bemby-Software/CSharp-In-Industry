import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

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
import { SocialListComponent } from './components/common/social-list/social-list.component';
import { SocialButtonComponent } from './components/common/social-button/social-button.component';
import { JudgedOnComponent } from './components/landing/judged-on/judged-on.component';
import { PrimaryTitleComponent } from './components/common/primary-title/primary-title.component';
import { TeamNameEntryComponent } from './components/landing/team-name-entry/team-name-entry.component';
import { FormsModule } from '@angular/forms';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { ParticipantsTableComponent } from './components/signup/participants-table/participants-table.component';
import { AddParticipantComponent } from './components/signup/add-participant/add-participant.component';

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
    MentorCardComponent,
    SocialListComponent,
    SocialButtonComponent,
    JudgedOnComponent,
    PrimaryTitleComponent,
    TeamNameEntryComponent,
    SignUpComponent,
    ParticipantsTableComponent,
    AddParticipantComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
