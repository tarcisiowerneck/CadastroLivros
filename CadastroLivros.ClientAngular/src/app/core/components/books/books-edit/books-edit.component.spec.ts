import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BooksEditComponent } from './books-edit.component';

describe('BooksEditComponent', () => {
  let component: BooksEditComponent;
  let fixture: ComponentFixture<BooksEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BooksEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BooksEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
