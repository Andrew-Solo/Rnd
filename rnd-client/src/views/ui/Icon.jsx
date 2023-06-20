import {SvgIcon} from "@mui/material";
import parse from 'html-react-parser';

export default function Icon({icon, ...props}) {
  return (
    <SvgIcon {...props}>
      <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" viewBox="0 -960 960 960">
        {parse(icon)}
      </svg>
    </SvgIcon>
  );
}