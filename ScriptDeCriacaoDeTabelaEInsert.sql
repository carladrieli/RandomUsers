CREATE TABLE public."User"
(
	uuid CHAR(36),
    email VARCHAR(100),
    name_first VARCHAR(50),
    name_last VARCHAR(50),
    gender VARCHAR(10),
    cell VARCHAR(20),

    PRIMARY KEY (uuid)
)
;

ALTER TABLE IF EXISTS public."User"
    OWNER to postgres;
	

INSERT INTO public."User"(
	uuid, email, name_first, name_last, gender, cell)
	VALUES 
	('550e8400-e29b-41d4-a716-446655440000', 'johndoe@example.com', 'John', 'Doe', 'male', '+1234567890'),
	('660e8400-e29b-41d4-a716-446655440111', 'janedoe@example.com', 'Jane', 'Doe', 'female', '+1987654321');
