INSERT INTO UserLog VALUES 
(1,'Уведомление','lox', 1)

CREATE OR REPLACE FUNCTION order_game()
    RETURNS trigger
    LANGUAGE 'plpgsql'
 
AS $orders$
    BEGIN     
        IF (TG_OP = 'DELETE') THEN
            UPDATE USERLOG SET LogMessage='delete order';
        ELSIF (TG_OP = 'UPDATE') THEN
            UPDATE USERLOG SET LogMessage='update order';
        ELSIF (TG_OP = 'INSERT') THEN
            UPDATE USERLOG SET LogMessage='create new order';
        END IF;
        RETURN NULL; 
    END;
$orders$;



CREATE TRIGGER orders
    AFTER UPDATE OR INSERT OR DELETE ON Orders
    FOR EACH ROW
    EXECUTE FUNCTION order_game();

INSERT INTO Orders VALUES
(6,100,'fk',1);

CREATE OR REPLACE FUNCTION order_vi()
    RETURNS TABLE(oderderid integer , orderprice decimal ,stateor character varying(30), usid integer) 
    LANGUAGE 'plpgsql'

AS $BODY$
begin
return query(
SELECT * FROM orders
  );
  end;
  
$BODY$;

select * from order_vi()

INSERT INTO StatusLog VALUES 
(1,'Notification','lox', 1)

CREATE OR REPLACE FUNCTION status_game()
    RETURNS trigger
    LANGUAGE 'plpgsql'
 
AS $status$
    BEGIN     
        IF (TG_OP = 'DELETE') THEN
            UPDATE USERLOG SET LogMessage='delete order';
        ELSIF (TG_OP = 'UPDATE') THEN
            UPDATE USERLOG SET LogMessage='update order';
        ELSIF (TG_OP = 'INSERT') THEN
            UPDATE USERLOG SET LogMessage='create new order';
        END IF;
        RETURN NULL; 
    END;
$status$;

CREATE TRIGGER Status
    AFTER UPDATE OR INSERT OR DELETE ON Status
    FOR EACH ROW
    EXECUTE FUNCTION status_game();

INSERT INTO Status VALUES
(6,'Ожидается',1);
