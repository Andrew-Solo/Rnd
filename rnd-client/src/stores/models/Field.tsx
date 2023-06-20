import Model from "./Model";
import {Accessibility, Enumerating, Interactivity, Type} from "./primitives";

export default class Field extends Model {
  readonly unitId: string | null
  readonly methodId: string | null
  type: Type
  accessibility: Accessibility
  interactivity: Interactivity
  enumerating: Enumerating
  nullable: boolean
  value: any | null

  constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    super(data);
    this.unitId = data.unitid ?? null;
    this.methodId = data.methodId ?? null;
    this.type = data.type ?? Type.Object;
    this.accessibility = data.accessibility ?? Accessibility.Space;
    this.interactivity = data.interactivity ?? Interactivity.Editable;
    this.enumerating = data.enumerating ?? Enumerating.None;
    this.nullable = data.nullable ?? false;
    this.value = data.value ?? null;

    // makeObservable(this, {
    //   unitId: false,
    //   methodId: false,
    // }, { autoBind: true });
  }
}