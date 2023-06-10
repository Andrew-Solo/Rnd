import {SvgIcon} from "@mui/material";
import { ReactComponent as HomeSvg } from "../assets/home.svg";
import { ReactComponent as GroupSvg } from "../assets/group.svg";
import { ReactComponent as HistorySvg } from "../assets/history.svg";
import { ReactComponent as AddSvg } from "../assets/add.svg";
import { ReactComponent as FilterListSvg } from "../assets/filterList.svg";
import { ReactComponent as FilterListSvg400 } from "../assets/filterList400.svg";
import { ReactComponent as AccountCircleSvg } from "../assets/accountCircle.svg";
import { ReactComponent as LockSvg } from "../assets/lock.svg";
import { ReactComponent as MailSvg } from "../assets/mail.svg";

export function Home(props) {
  return (
    <SvgIcon {...props}>
      <HomeSvg/>
    </SvgIcon>
  );
}

export function Group(props) {
  return (
    <SvgIcon {...props}>
      <GroupSvg/>
    </SvgIcon>
  );
}

export function History(props) {
  return (
    <SvgIcon {...props}>
      <HistorySvg/>
    </SvgIcon>
  );
}

export function Add(props) {
  return (
    <SvgIcon {...props}>
      <AddSvg/>
    </SvgIcon>
  );
}

export function FilterList({weight = 300, ...props}) {

  const svg = weight === 400
    ? <FilterListSvg400/>
    : <FilterListSvg/>

  return (
    <SvgIcon {...props}>
      {svg}
    </SvgIcon>
  );
}

export function AccountCircle(props) {
  return (
    <SvgIcon {...props}>
      <AccountCircleSvg/>
    </SvgIcon>
  );
}

export function Lock(props) {
  return (
    <SvgIcon {...props}>
      <LockSvg/>
    </SvgIcon>
  );
}

export function Mail(props) {
  return (
    <SvgIcon {...props}>
      <MailSvg/>
    </SvgIcon>
  );
}
