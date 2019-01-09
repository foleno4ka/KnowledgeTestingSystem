import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestSolveComponent } from './test-solve.component';

describe('TestSolveComponent', () => {
  let component: TestSolveComponent;
  let fixture: ComponentFixture<TestSolveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestSolveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestSolveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
