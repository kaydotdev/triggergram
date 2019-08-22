import { Component, Input, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { TagBarService } from './tag-bar.service';
import { Tag } from 'src/app/dto/Tag';

@Component({
    selector: 'tag-bar',
    templateUrl: './tag-bar.component.html',
    providers: [TagBarService, CookieService]
})
export class TagBarComponent implements OnInit {
    @Input() PostId: number;
    tags: Tag[];
  
    constructor(private tagBarService: TagBarService,
                private router: Router) {
    }

    ngOnInit(): void {
        this.tagBarService.getPostTags(this.PostId).subscribe(
            (data) => this.tags = Array.prototype.slice.call(data),
            (error) => console.log(error)
        );
    }
}