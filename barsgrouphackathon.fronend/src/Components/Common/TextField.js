import { TextField } from '@material-ui/core';
import React from 'react';

/**
 * Текстовое поле
 * @param props Пропсы
 */
function TextFieldComponent({
    label, name, value, required, error, onChange, type
}) {
    return (
        <TextField style={{ flex: 1 }}
            error={!!error}
            label={label}
            name={name}
            required={required}
            value={value || ''}
            helperText={error}
            onChange={x => onChange && onChange(x.target.value)}
            type={type || 'text'} />
    );
};

export default TextFieldComponent;
