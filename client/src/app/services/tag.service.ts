import { Injectable, Optional, inject } from '@angular/core';
import { NewTag, Tag } from '../models/interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class TagService {
  private http = inject(HttpClient);
  rootApiUrl = environment.apiUrl;
  tagApiUrl = this.rootApiUrl + "/Tag";

  getTags(): Observable<any> {
    return this.http.get(this.tagApiUrl);
  }
  addTag(tag: NewTag): Observable<any> {
    return this.http.post(this.tagApiUrl, tag);
  }
  saveTag(tag: Tag): Observable<any> {
    try {
      let tagId = tag.id;
      if(tagId === null) {
        console.error("Null Tag Id is not allowed");
      }
      return this.http.put(`${this.tagApiUrl}/${tagId}`, tag);

    } catch(error) {
      return throwError(() => new Error('Tag id not found'));
    }
  }
  deleteTag(tag: Tag): Observable<any> {
    try {
      let tagId = tag.id;
      if(tagId === null) {
        console.error("Null Tag Id is not allowed");
      }
      return this.http.delete(`${this.tagApiUrl}/${tagId}`);

    } catch(error) {
      return throwError(() => new Error('Tag id not found'));
    }
  }
}
