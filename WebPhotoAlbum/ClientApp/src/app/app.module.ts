import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ImageCardComponent } from './image-card/image-card.component';
import { EmojiBarComponent } from './emoji-bar/emoji-bar.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { LoginBarComponent } from './login-bar/login-bar.component';
import { TagSearchCatalogComponent } from './tag-search-catalog/tag-search-catalog.component'
import { PostCatalogComponent } from './posts-catalog/post-catalog.component';
import { PostCatalogCardComponent } from './posts-catalog/post-catalog-card.component';
import { PostCatalogAddComponent } from './posts-catalog/post-catalog-add.component';
import { TagBarComponent } from './tag-bar/tag-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ImageCardComponent,
    EmojiBarComponent,
    LoginFormComponent,
    LoginBarComponent,
    PostCatalogComponent,
    PostCatalogCardComponent,
    PostCatalogAddComponent,
    TagSearchCatalogComponent,
    TagBarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: PostCatalogComponent, pathMatch: 'full' },
      { path: 'search-tag', component: TagSearchCatalogComponent },
      { path: 'login', component: LoginFormComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
