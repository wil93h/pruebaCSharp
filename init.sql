-- products
CREATE TABLE IF NOT EXISTS products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION add_product(
    p_name VARCHAR,
    p_description TEXT,
    p_price DECIMAL,
    p_stock INT
) RETURNS VOID AS $$
BEGIN
    INSERT INTO products (name, description, price, stock, created_at, updated_at)
    VALUES (p_name, p_description, p_price, p_stock, NOW(), NOW());
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_product(
    p_id INT,
    p_name VARCHAR,
    p_description TEXT,
    p_price DECIMAL,
    p_stock INT
) RETURNS VOID AS $$
BEGIN
    UPDATE products 
    SET name = p_name, 
        description = p_description, 
        price = p_price, 
        stock = p_stock, 
        updated_at = NOW()
    WHERE id = p_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_product(p_id INT) RETURNS VOID AS $$
BEGIN
    DELETE FROM products WHERE id = p_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_products() RETURNS SETOF products AS $$
BEGIN
    RETURN QUERY SELECT * FROM products;
END;
$$ LANGUAGE plpgsql;

-- users
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION add_user(
    p_username VARCHAR,
    p_email VARCHAR,
    p_password_hash TEXT
) RETURNS VOID AS $$
BEGIN
    INSERT INTO users (username, email, password_hash, created_at)
    VALUES (p_username, p_email, p_password_hash, NOW());
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_user(
    p_id INT,
    p_username VARCHAR,
    p_email VARCHAR
) RETURNS VOID AS $$
BEGIN
    UPDATE users 
    SET username = p_username, 
        email = p_email
    WHERE id = p_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_user(p_id INT) RETURNS VOID AS $$
BEGIN
    DELETE FROM users WHERE id = p_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_all_users() RETURNS SETOF users AS $$
BEGIN
    RETURN QUERY SELECT * FROM users;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION get_user_by_email(email_param VARCHAR)
RETURNS TABLE (
    id INT,
    username VARCHAR,
    email VARCHAR,
    password_hash VARCHAR,
    created_at TIMESTAMP
)
AS
$$
BEGIN
    RETURN QUERY
    SELECT u.id, u.username, u.email, u.password_hash, u.created_at
    FROM users u
    WHERE u.email = email_param;
END;
$$ LANGUAGE plpgsql;