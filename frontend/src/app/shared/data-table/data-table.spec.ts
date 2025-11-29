import { signal } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DataTable } from './data-table';
import { TableColumnConfig } from './data-table.types';

interface Row { id: string; name: string; value: string; }

describe('DataTable', () => {
    let fixture: ComponentFixture<DataTable<Row>>;
    let component: DataTable<Row>;

    const rows = signal<Row[]>([
        { id: '1', name: 'Alpha', value: 'A' },
        { id: '2', name: 'Beta', value: 'B' },
        { id: '3', name: 'Gamma', value: 'C' }
    ]);

    const cols: TableColumnConfig<Row>[] = [
        { field: 'name', label: 'Name', type: 'text', sortable: true },
        { field: 'value', label: 'Value', type: 'text', sortable: true }
    ];

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [DataTable<Row>]
        }).compileComponents();

        fixture = TestBed.createComponent(DataTable<Row>);
        component = fixture.componentInstance;
        (component as any).data = { value: () => rows() } as any; // provide a Signal-like accessor
        (component as any).columns = { value: () => cols } as any;
        (component as any).features = {
            value: () => ({ editable: true, quicksearch: true, sortable: true })
        } as any;
        fixture.detectChanges();
    });

    it('renders rows', () => {
        const compiled = fixture.nativeElement as HTMLElement;
        const bodyRows = compiled.querySelectorAll('tbody tr');
        expect(bodyRows.length).toBe(3);
    });

    it('filters by quicksearch', () => {
        component['query'].set('alp');
        fixture.detectChanges();
        const compiled = fixture.nativeElement as HTMLElement;
        const bodyRows = compiled.querySelectorAll('tbody tr');
        expect(bodyRows.length).toBe(1);
    });

    it('sorts by column', () => {
        const firstCol = component['orderedColumns']()[0];
        component['toggleSort'](firstCol);
        fixture.detectChanges();
        // verify first cell after sorting
        // basic assertion by checking first row's first cell
        const firstCell = (fixture.nativeElement as HTMLElement)
            .querySelector('tbody tr td:first-child') as HTMLElement;
        expect(firstCell.textContent?.trim()).toBe('Alpha');
    });

    it('emits save on inline edit', () => {
        spyOn(component.save, 'emit');
        // start edit on first row
        component['startEdit'](rows()[0]);
        component['setEditValue'](cols[0], 'Alpha Edited');
        component['saveRow']();
        expect(component.save.emit).toHaveBeenCalled();
    });
});
