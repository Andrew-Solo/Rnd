import {Button, Stack} from "@mui/material";

export default function ActionsContainer({actions, editing, setEditing}) {
  return(
    <Stack spacing={2}>
      {editing
        ? <>
          <Button variant="contained" onClick={() => setEditing(!editing)}>
            Сохранить
          </Button>
          <Button variant="contained" color="secondary" onClick={() => setEditing(!editing)}>
            Отмена
          </Button>
          <Button variant="contained" color="error">
            Удалить
          </Button>
        </>
        : <Button variant="contained" onClick={() => setEditing(!editing)}>Редактировать</Button>
      }
      {actions.filter(() => !editing).map(action =>
        <Button variant="contained" color="secondary" key={action.name} href={action.href}>
          {action.label}
        </Button>
      )}
    </Stack>
  )
}