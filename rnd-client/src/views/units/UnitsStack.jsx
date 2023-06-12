import {Stack} from "@mui/material";
import Unit from "./Unit";

export default function UnitsStack({data, fields, editing}) {
  return(
    <Stack spacing={2}>
      {fields
        .filter(field => editing ? field.editable : field.visible)
        .map(field => <Unit key={field.name} editing={editing} value={data[field.name]} {...field}/>)}
    </Stack>
  )
}