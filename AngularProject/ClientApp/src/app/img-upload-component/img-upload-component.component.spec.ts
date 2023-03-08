import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImgUploadComponentComponent } from './img-upload-component.component';

describe('ImgUploadComponentComponent', () => {
  let component: ImgUploadComponentComponent;
  let fixture: ComponentFixture<ImgUploadComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImgUploadComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ImgUploadComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
