import Model from "./Model";
import {Accessibility, Enumerating, Prototype, Type} from "./primitives";

export default class Field extends Model {
  readonly unitId: string
  prototype: Prototype
  type: Type
  enumerating: Enumerating
  accessibility: Accessibility
  readonly: boolean
  hidden: boolean
  modifiable: boolean
  nullable: boolean
  value: any | null

  constructor(data: {id: string, name: string, path: string, unitId: string, [key:string]: any}) {
    super(data);
    this.unitId = data.unitId;
    this.prototype = data.prototype ?? Prototype.Property;
    this.type = data.type ?? Type.Object;
    this.enumerating = data.enumerating ?? Enumerating.None;
    this.accessibility = data.accessibility ?? Accessibility.Space;
    this.readonly = data.readonly ?? false;
    this.hidden = data.hidden ?? false;
    this.modifiable = data.modifiable ?? false;
    this.nullable = data.nullable ?? false;
    this.value = data.value ?? null;
  }
}