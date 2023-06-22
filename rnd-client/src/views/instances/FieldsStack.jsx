import {Stack} from "@mui/material";
import Field from "./Field";

export default function FieldsStack({data, fields, editing}) {
  return(
    <Stack spacing={2}>
      {fields
        .filter(field => editing ? field.editable : field.visible)
        .map(field => <Field key={field.name} editing={editing} value={data[field.name]} {...field}/>)}
    </Stack>
  )
}