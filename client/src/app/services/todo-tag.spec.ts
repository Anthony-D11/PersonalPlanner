import { TestBed } from '@angular/core/testing';

import { TodoTag } from './todo-tag.service';

describe('TodoTag', () => {
  let service: TodoTag;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TodoTag);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
