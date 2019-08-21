import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PostCatalogService } from './post-catalog.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { PostPreview } from 'src/app/dto/PostPreview';

@Component({
    selector: 'post-catalog-add',
    templateUrl: './post-catalog-add.component.html',
    providers: [FormBuilder, PostCatalogService, CookieService]
})
export class PostCatalogAddComponent implements OnInit {
    postPreview: PostPreview[] = [];
    uploadForm: FormGroup;  
  
    constructor(private formBuilder: FormBuilder,
                private postCatalogService: PostCatalogService,
                private router: Router) {
        this.uploadForm = this.formBuilder.group({
            description: [''],
            photo: ['']
        });
    }

    onFileSelected(event)
    {
        if (event.target.files.length > 0) {
            const file = event.target.files[0];
            this.uploadForm.get('photo').setValue(file);
        }
    }

    onDescriptionChanged(event)
    {
        this.uploadForm.get('description').setValue(event.target.value);
    }

    submitPost()
    {
        const formData = new FormData();

        formData.append('photoFile', this.uploadForm.get('photo').value);
        formData.append('postDescription', this.uploadForm.get('description').value);
    
        console.log(formData);

        //this.postCatalogService.addNewPost(formData).subscribe(
         //   (res) => console.log(res),
         //   (err) => console.log(err)
        //);
    }

    ngOnInit(): void {
        throw new Error("Method not implemented.");
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