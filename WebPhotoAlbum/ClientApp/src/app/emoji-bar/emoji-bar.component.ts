import { Component } from '@angular/core';
import { EmojiService } from './emoij.service';
import { CookieService } from 'ngx-cookie-service';
import {Router} from '@angular/router';
import { Emoji } from 'src/app/dto/Emoji';

@Component({
    selector: 'emoji-bar',
    templateUrl: './emoji-bar.component.html',
    providers: [EmojiService, CookieService]
})
export class EmojiBarComponent {
    emojis: Emoji[];

    constructor(private emojiService: EmojiService,
                private router: Router) 
    {
        this.emojiService.getEmojis().subscribe(
            (data) => this.emojis = Array.prototype.slice.call(data),
            (error) => this.handleServerError(error.status)
        );
    }

    handleServerError(statusCode: number): void
    {
        switch (statusCode) 
        {
            case 401:
                this.router.navigate(['/login']);
                break;
            default:
                console.error("Server returned code: ${statusCode}");
        }
    }
}