import {TextField, Stack, Typography} from "@mui/material";
import {observer} from "mobx-react-lite";

const StringField = observer(({form, field}) => {
  const editing = form.editing;
  const value = form.data[field.name];

  if (editing) return (
    <TextField label={field.title} value={value ?? ""} helperText=" "/>
  )

  return (
    <Stack spacing={1}>
      <Typography variant="caption" color="text.secondary">
        {field.title}
      </Typography>
      <Typography>
        {value ?? ""}
      </Typography>
    </Stack>
  );
});

export default StringField