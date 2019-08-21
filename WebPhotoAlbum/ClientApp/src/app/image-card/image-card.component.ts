import { Component } from '@angular/core';
import { ImageCardService } from './image-card.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { PostToView } from 'src/app/dto/PostToView';
import { PhotoPost } from 'src/app/dto/PhotoPost';

@Component({
    selector: 'image-card',
    templateUrl: './image-card.component.html',
    providers: [ImageCardService, CookieService]
})
export class ImageCardComponent {
    postToView: PostToView;
  
    constructor(private emojiService: ImageCardService,
                private router: Router) {
        this.getPostToViewByIndex(0);
    }

    getPostToViewByIndex(index: number)
    {
        this.postToView = new PostToView();
        this.emojiService.getUserPostByOrderIndex(index).subscribe(
            (data) => {
                let postHeader: PhotoPost;
                postHeader = Array.prototype.slice.call(data)[0]
                this.postToView.id = postHeader.id;
                this.postToView.description = postHeader.description;
                this.postToView.postingDate = postHeader.postingDate;

                this.emojiService.getPostPhoto(this.postToView.id).subscribe(
                    (data) => {
                        let postHeader = {
                            id:1, 
                            name:"", 
                            source:""
                        };
                        Object.assign(postHeader, data);

                        this.postToView.photoName = postHeader.name;
                        this.postToView.photoSource = postHeader.source;
                    },
                    (error) => this.handleServerError(error.status)
                );
            },
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