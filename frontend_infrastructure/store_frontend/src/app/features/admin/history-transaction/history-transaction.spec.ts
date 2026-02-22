import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoryTransaction } from './history-transaction';

describe('HistoryTransaction', () => {
  let component: HistoryTransaction;
  let fixture: ComponentFixture<HistoryTransaction>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HistoryTransaction]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HistoryTransaction);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
