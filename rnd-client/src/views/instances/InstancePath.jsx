import {Breadcrumbs, Link, Typography} from "@mui/material";

export default function InstancePath() {
  return (
    <Breadcrumbs aria-label="breadcrumb">
      <Link color="text.secondary" underline="hover">Игры</Link>
      <Typography color="text.primary">Мрак</Typography>
    </Breadcrumbs>
  );
}