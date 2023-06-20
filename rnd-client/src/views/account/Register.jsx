import {Box, Button, Stack} from "@mui/material";
import {AccountCircle, Lock} from "../icons";
import IconTextField from "./IconTextField";
import {observer} from "mobx-react-lite";
import Form from "../../stores/models/Form";

const Register = observer((props: {form: Form}) => {
  const {createInputProps, handleSubmit} = props.form;

  return (
    <form onSubmit={handleSubmit}>
      <Stack width={1} gap={2}>
        {/*TODO увеличить бордер, сделать по ярче*/}
        <IconTextField {...createInputProps("login")} placeholder="Логин" icon={<AccountCircle/>}/>
        <IconTextField {...createInputProps("password")} placeholder="Пароль" type="password" icon={<Lock/>}/>
        <IconTextField {...createInputProps("confirmPassword")} placeholder="Повтор пароля" type="password" icon={<Lock/>}/>
        <Box gap={4} display="flex">
          <Button fullWidth variant="contained" color="secondary" href="/account/login">Вход</Button>
          <Button type="submit" fullWidth variant="contained">Регистрация</Button>
        </Box>
      </Stack>
    </form>
  );
});
export default Register