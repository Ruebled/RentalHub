import subprocess
import oracledb as cx_Oracle

def run_sql_script_cxoracle(script_path, dsn, user, password, sqlplus=False):
    try:
        connection = cx_Oracle.connect(user=user, password=password, dsn=dsn)
        cursor = connection.cursor()

        with open(script_path, 'r') as file:
            sql_script = file.read()

        if sqlplus:
            # Split script by '/' to handle PL/SQL blocks or command batches
            sql_commands = sql_script.split('/')
        else:
            # Split script by ';' to handle individual SQL statements
            sql_commands = sql_script.split(';')

        for command in sql_commands:
            command = command.strip()
            if command:
                try:
                    cursor.execute(command)
                    print(f"Executed SQL command/block: {command[:60]}...")
                except cx_Oracle.Error as error:
                    print(f"Error executing SQL command/block: {command[:60]}... - {error}")

        connection.commit()
        print(f"Executed SQL script: {script_path}")

    except cx_Oracle.Error as error:
        print(f"Error executing SQL script {script_path}: {error}")
        connection.rollback()

    finally:
        cursor.close()
        connection.close()

def run_python_script(script_path):
    try:
        subprocess.run(['python', script_path], check=True)
        print(f"Executed Python script: {script_path}")
    except subprocess.CalledProcessError as e:
        print(f"Error executing Python script {script_path}: {e}")

if __name__ == "__main__":
    # Database connection details
    dsn = cx_Oracle.makedsn('localhost', '1521', service_name='ORCL')
    user = 'test'
    password = 'test'
    
    # List of script paths to run in order (SQL and Python)
    scripts = [
        ('sqlplus', 'DropData.sql'),
        ('sql', 'ObjectCreation.sql'),
        ('python', 'InsertUserImages.py'),
        ('sql', 'TablesFill.sql'),
        ('python', 'InsertApartmentImages.py')
    ]

    # Run scripts in the specified order
    for script_type, script_path in scripts:
        if script_type in ('sqlplus', 'sql'):
            run_sql_script_cxoracle(script_path, dsn, user, password, sqlplus=(script_type == 'sqlplus'))
        elif script_type == 'python':
            run_python_script(script_path)
