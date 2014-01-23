using System;
using System.IO;
using System.Text;

namespace FileDestory 
{
	public class FileDestory 
	{
		public FileDestory(string path) 
		{
			//Console.WriteLine("������Ҫ������ļ�,֧����ק:");
			//String path = Console.ReadLine();
			FileStream filestream=null;
			BinaryWriter objBinaryWriter=null;
			try
			{
				if (File.Exists(path))
				{
					try
					{
						//���ļ�
						filestream=new FileStream(path,FileMode.Create);

						///setAccessControl.ReadAndWrite
						///

						//����д���ļ���
						objBinaryWriter = new BinaryWriter(filestream);
						//���ֽ�����ʽд���ļ�
						
						byte [] filecontent=new UTF8Encoding(true).GetBytes("");
						//path.Length����ֱ�Ӷ�ȡ�ѽ����ļ���������	
						for(int index =0; index<path.Length;index++)
						{
							objBinaryWriter.Write(filecontent);
						}
						Console.WriteLine("�ļ��Ѿ�����");
						
						try
						{
							
							//�ر��ļ�������
							objBinaryWriter.Close();
							filestream.Close();
						}
						catch(Exception)
						{
							Console.WriteLine("δ����Ч�պ��ļ���");
						}		
						//ɾ���ļ�
						File.Delete(path);

					}
					catch(Exception)
					{
						Console.WriteLine("����д���ļ�ʧ��!");
					}

				}
				else
				{
					Console.WriteLine("Inviabled File(s) Specified");
				}
                
			}
			catch (Exception e)
			{
				Console.Out.WriteLine("occured an Exception!" + "\n" + e.Message);
			}        
		}
	}
}