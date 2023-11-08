import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PalestranteComponentComponent } from './palestrante-component.component';

describe('PalestranteComponentComponent', () => {
  let component: PalestranteComponentComponent;
  let fixture: ComponentFixture<PalestranteComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PalestranteComponentComponent]
    });
    fixture = TestBed.createComponent(PalestranteComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
