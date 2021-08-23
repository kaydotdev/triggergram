import { useState, MouseEvent } from 'react';
import {
    Button,
    MenuItem,
    Menu
} from '@material-ui/core';
import {
    AccountCircle as AccountCircleIcon,
    Settings as SettingsIcon,
    PowerSettingsNew as PowerSettingsNewIcon,
    Edit as EditIcon
} from '@material-ui/icons';
import {
    createStyles,
    makeStyles,
    Theme
} from '@material-ui/core/styles';


const useStyles = makeStyles((_: Theme) =>
    createStyles({
        appBarButton: {
            color: '#fff'
        },
        appBarIcon: {
            marginRight: 4
        }
    })
);

function ProfileMenu() {
    const classes = useStyles();

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleClick = (event: MouseEvent<HTMLButtonElement>) => { setAnchorEl(event.currentTarget); };
    const handleClose = () => { setAnchorEl(null); };

    return (
        <div>
            <Button
            aria-label="profile"
            className={classes.appBarButton}
            aria-controls="profile-menu"
            aria-haspopup="true"
            onClick={handleClick}
            >
                <AccountCircleIcon />
            </Button>
            <Menu
                id="profile-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <MenuItem onClick={handleClose}>
                    <EditIcon className={classes.appBarIcon} /> Edit Profile
                </MenuItem>
                <MenuItem onClick={handleClose}>
                    <SettingsIcon className={classes.appBarIcon} /> Settings
                </MenuItem>
                <MenuItem onClick={handleClose}>
                    <PowerSettingsNewIcon className={classes.appBarIcon} /> Logout
                </MenuItem>
            </Menu>
        </div>
    );
}

export default ProfileMenu;
