import Model from "./Model";

export default class Instance extends Model {
  readonly creatorId: string
  readonly unitId: string
  readonly spaceId: string | null
  properties: {[name:string]: any}

  constructor(data: {id: string, name: string, path: string, creatorId: string, unitId: string, [key:string]: any}) {
    super(data);
    this.creatorId = data.creatorId;
    this.unitId = data.unitId;
    this.spaceId = data.spaceId ?? null;
    this.properties = data.properties ?? {};
  }
}